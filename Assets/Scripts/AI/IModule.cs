using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IModule
{
    public void Register(AICharacter character);

    // TODO: Implement in AICharacter. On Death?
    public void Deregister();
}
