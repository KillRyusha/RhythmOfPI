
using System.Collections;
using UnityEngine;
using Zenject;

public class Bootstrap : MonoBehaviour, ICoroutineRunner
{
    [SerializeField] private Player _player;
    [SerializeField] private LastPlatfrom _lastPlatform;
    [SerializeField] private PlayerAnimationPlayer _playerAnimationPlayer;
    [SerializeField] private LevelSetUpper _levelSetupper;
    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private SongManager _levelCardManager;
    [SerializeField] private JsonParser _jsonParser;
    [SerializeField] private string Json;

    [Inject] private PrefabFactory<Note> _noteFactory;
    [Inject] private PrefabFactory<Platform> _platformFactory;

    private const int NOTES_AMOUNT = 60;
    private const int PLATFORMS_AMOUNT = 5;
    private const int GAME_SPEED = 50;
    private const int TACT_LENGTH = 4;
    private const float GAME_START_DELAY = 3.5f;

    private StateMachine _gameStateMachine;
    private StateMachine _playerStateMachine;
    private PlatformPlacer _platformPlacer;
    private GameSpeedService _gameSpeedService = new GameSpeedService();
    private WebService _webService;
    void Awake()
    {
        _webService = new WebService(this);
        _webService.StartGame((song) => { StartGame(song); }, Json);
    }

    private void OnEnable()
    {
        _lastPlatform.OnPlatformEnter += () => { _gameSpeedService.DecreaseSpeed(); };
    }
    private void OnDisable()
    {
        _platformPlacer.UnSubscribe(EndGame);
        _lastPlatform.OnPlatformEnter -= () => { _gameSpeedService.DecreaseSpeed(); };
    }

    void Update()
    {
        _gameStateMachine?.UseStateUpdate();
        _playerStateMachine?.UseStateUpdate();
        _gameSpeedService?.Update(Time.deltaTime);
    }
    private IEnumerator StartRun()
    {
        yield return new WaitForSeconds(GAME_START_DELAY);
        _playerStateMachine.ChangeState(typeof(PlayerInGameState));
        _gameStateMachine.ChangeState(typeof(GameState));
    }
    private void EndGame()
    {
        _playerStateMachine.ChangeState(typeof(PlayerEndState)); 
        _gameStateMachine.ChangeState(typeof(GameEndState));
    }
    private void SetUpStateMachines()
    {
        _gameStateMachine = new StateMachine();
        _playerStateMachine = new StateMachine();
        _gameStateMachine.SetStateInitializer(new GameStateInitializer(_gameStateMachine, _platformPlacer, _gameSpeedService, _levelSetupper));
        _playerStateMachine.SetStateInitializer(new PlayerStateInitializer(_playerStateMachine, _playerAnimationPlayer, _player));

        _gameStateMachine.ChangeState(typeof(InitState));
        _playerStateMachine.ChangeState(typeof(PlayerInitState));
    }
    private void SetUpPlatformPlacer()
    {
        ObjectPool<Note> notesPool = new ObjectPool<Note>(_noteFactory);
        ObjectPool<Platform> platformsPool = new ObjectPool<Platform>(_platformFactory);
        notesPool.SetUpPool(NOTES_AMOUNT);
        platformsPool.SetUpPool(PLATFORMS_AMOUNT);
        int level = CurrentSongInfoSingleton.Instance.Level;
        NotesPlacer notesPlacer = new NotesPlacer(notesPool, TACT_LENGTH, _jsonParser.GetLevelNumbers(level));
        _platformPlacer = new PlatformPlacer(platformsPool, notesPlacer, _gameSpeedService, _lastPlatform);
        _platformPlacer.Subscribe(EndGame);
    }
    private void StartGame(SongInfoJson song)
    {
        _levelCardManager.SetSong(song, (startGame) =>
        {
            if (startGame)
            {
                SetUpPlatformPlacer();
                SetUpStateMachines();
                StartCoroutine(StartRun());
                _loadingScreen.SetActive(false);
            }
        }); 
    }
}
