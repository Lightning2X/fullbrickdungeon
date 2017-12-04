﻿
using System;
using System.Collections.Generic;
abstract class Weapon : AnimatedGameObject
{
    protected ClassType classType;
    Character owner;
    Ability BasicAttack;
    TimedAbility mainAbility;
    SpecialAbility specialAbility;
    private int attack, goldCost;
    protected Weapon(Character owner, ClassType classType, string id, string assetName): base(1, id)
    {
        this.owner = owner;
        this.classType = classType;
    }

    public virtual void Attack()
    {
        BasicAttack.Use();
        CollisionChecker(this.CurrentAnimation);
    }

    public virtual void UseMainAbility()
    {
        mainAbility.Use();
        CollisionChecker(this.CurrentAnimation);
    }

    public virtual void UseSpecialAbility()
    {
        specialAbility.Use();
        CollisionChecker(this.CurrentAnimation);
    }

    protected void CollisionChecker(Animation animation)
    {
        while (!(animation.AnimationEnded))
        {
            GameObjectList monsterList = GameWorld.Find("monsterLIST") as GameObjectList;
            foreach (Monster monsterobj in monsterList.Children)
            {
                if (monsterobj.CollidesWith(this))
                {
                    monsterobj.TakeDamage(owner.Attributes.Attack + this.AttackDamage);
                    if (monsterobj.IsDead)
                    {
                        owner.Attributes.Gold += monsterobj.Attributes.Gold;
                    }
                }
            }
        }
    }


    public int AttackDamage
    {
        get { return attack; }
        protected set { attack = value; }
    }
    public int GoldWorth
    {
        get { return goldCost; }
        protected set { goldCost = value; }
    }

    public Character Owner
    {
        get { return owner; }
    }

    public ClassType Type
    {
        get { return classType; }
    }
}

