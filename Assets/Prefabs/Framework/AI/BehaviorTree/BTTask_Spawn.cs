using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTask_Spawn : BTNode
{
    private BehaviorTree behaviorTree;
    private SpawnerComponent spawnerComponent;
    public BTTask_Spawn(BehaviorTree behaviorTree)
    {
        this.behaviorTree = behaviorTree;
    }
    protected override E_NodeResult Excute()
    {
        spawnerComponent = behaviorTree.GetComponent<SpawnerComponent>();
        if (spawnerComponent!=null)
        {
            bool a=spawnerComponent.StartSpawn();
            return a ? E_NodeResult.Success : E_NodeResult.Failure;
        }
        return E_NodeResult.Failure;
    }
}
