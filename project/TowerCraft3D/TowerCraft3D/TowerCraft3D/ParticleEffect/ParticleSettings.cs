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
        public int minLife = 200;
        public int maxLife = 400;

        // Particles per round
        public int minParticlesPerRound = 10;
        public int maxParticlesPerRound = 50;

        // Round time
        public int minRoundTime = 10;
        public int maxRoundTime = 40;

        // Number of particles
        public int minParticles = 50;
        public int maxParticles = 100;
    }
}
