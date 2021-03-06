﻿using CryEngine;

namespace CryGameCode
{
	[Entity(Category = "Others", EditorHelper = "Objects/Tanks/tank_chassis.cgf")]
	public class SpawnPoint : Entity
	{
		public SpawnPoint()
		{
			LastSpawned = -1;
		}

		public bool CanSpawn
		{
			get
			{
				return (Time.FrameStartTime - LastSpawned) > SpawnDelay * 1000 || LastSpawned == -1;
			}
		}

		public bool TrySpawn(EntityBase entity)
		{
			if (entity == null)
				throw new System.ArgumentNullException("entity");
			if (!Game.IsServer)
				return false;

			if (CanSpawn)
			{
				LastSpawned = Time.FrameStartTime;

				var pos = Position;
				var rot = Rotation;

				entity.Position = pos;
				entity.Rotation = rot;

				return true;
			}

			return false;
		}

		public float LastSpawned { get; private set; }

		/// <summary>
		/// Min delay between ability to spawn entities per spawnpoint.
		/// </summary>
		[EditorProperty]
		public float SpawnDelay = 3;

		[EditorProperty]
		public string Team = "red";
	}
}