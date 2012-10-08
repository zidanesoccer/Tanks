﻿using CryEngine;
using CryGameCode.Entities;

namespace CryGameCode.Projectiles
{
	public abstract class Projectile : Entity
	{
		public override void OnSpawn()
		{
			LoadObject(Model);

			Physics.Type = PhysicalizationType.Rigid;
			Physics.Mass = Mass;
			Physics.Slot = 0;

			Launch();
		}

		public virtual void Launch()
		{
			var dir = Rotation.Column1;
			Physics.AddImpulse(dir * Speed);
		}

		protected override void OnCollision(EntityId targetEntityId, Vec3 hitPos, Vec3 dir, short materialId, Vec3 contactNormal)
		{
			var effect = ParticleEffect.Get(Effect);
			effect.Spawn(hitPos);

			// Id 0 is the terrain
			if(targetEntityId != 0)
			{
				var target = Entity.Get(targetEntityId) as DamageableEntity;

				if(target != null)
					target.Damage(Damage, DamageType);
			}

			Remove();
		}

		public abstract string Model { get; }
		public abstract float Mass { get; }
		public abstract float Speed { get; }
		public abstract float Damage { get; }
		public abstract string Effect { get; }
		public abstract DamageType DamageType { get; }
	}
}
