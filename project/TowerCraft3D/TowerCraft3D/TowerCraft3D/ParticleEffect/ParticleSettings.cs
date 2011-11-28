using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TowerCraft3D
{
    class ParticleSettings
    {
        // Size of particle
        public int maxSize = 2;
    }

    class ParticleExplosionSettings
    {
        // Life of particles
        public int minLife = 350;
        public int maxLife = 700;

        // Particles per round
        public int minParticlesPerRound = 16;
        public int maxParticlesPerRound = 50;

        // Round time
        public int minRoundTime = 20;
        public int maxRoundTime = 60;

        // Number of particles
        public int minParticles = 50;
        public int maxParticles = 100;
    }
}
