using System;
using System.Collections.Generic;

public interface IStateInitializer
{
    Dictionary<Type, AState> Initialize();
}
