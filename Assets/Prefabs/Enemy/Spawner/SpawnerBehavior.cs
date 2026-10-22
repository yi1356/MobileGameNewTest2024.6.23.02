using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBehavior : BehaviorTree
{
    protected override void ConstructTree(out BTNode Root)
    {
        BTTask_Spawn spawn = new BTTask_Spawn(this);
        CooldownDecorator coolDown = new CooldownDecorator(spawn, 3f);
        BlackboardDecorator blackboardDecorator = new BlackboardDecorator(coolDown,this,"Target");
        Root = blackboardDecorator;
    }
}
