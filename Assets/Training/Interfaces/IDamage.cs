﻿namespace Interfaces
{
    public interface IDamage
    {
        int Health { get; set; }
        void Damage(int damageAmount);

    }
}