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
        public int minLife = 500;
        public int maxLife = 1000;

        // Particles per round
        public int minParticlesPerRound = 50;
        public int maxParticlesPerRound = 100;

        // Round time
        public int minRoundTime = 16;
        public int maxRoundTime = 50;

        // Number of particles
        public int minParticles = 500;
        public int maxParticles = 1000;
    }
}
