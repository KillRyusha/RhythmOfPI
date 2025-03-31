using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private Note _notePrefab;
    [SerializeField] private Platform _platformPrefab;
    [SerializeField] private LastPlatfrom _lastPlatform;
    [SerializeField] private NoteUIController _noteUIController;
    public override void InstallBindings()
    {
        Container.Bind<NoteUIController>()
            .FromInstance(_noteUIController)
            .AsSingle();
        Container.Bind<PrefabFactory<Note>>()
           .ToSelf()
           .AsTransient()
           .WithArguments(_notePrefab, Container);
        Container.Bind<PrefabFactory<Platform>>()
           .ToSelf()
           .AsTransient()
           .WithArguments(_platformPrefab, Container);
        Container.Bind<LastPlatfrom>()
            .FromInstance(_lastPlatform) // Передаем ссылку на объект, который уже есть на сцене
            .AsSingle();
    }
}
