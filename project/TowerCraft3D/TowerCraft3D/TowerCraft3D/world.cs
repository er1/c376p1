using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerCraft3D
{

    // Cube is completely plotted by myself
    //Learned from drawing a textured square from a tutorial by basically like opengl
    // class idea taken from "Switch on the Code "but only took the fixed plot points from the code

    class world
    {
        //protected int worldSize;
        protected Vector3 cubeSize;
        protected Vector3 cubePosition = new Vector3(0, 0, 0);
        protected VertexPositionTexture[] cubeVertices;


        public VertexPositionTexture[] getCubeVertices()
        {
            return cubeVertices;
        }

        //Construct Cube
        public void setCubeVertices(int worldSize)
        {
            cubeSize = new Vector3(worldSize, worldSize, worldSize);
            cubeVertices = new VertexPositionTexture[26];

            //Texture Mappting
            Vector2 topLeft = new Vector2(0.0f, 0.0f);  //0
            Vector2 bottomLeft = new Vector2(0.0f, 1.0f); //2
            Vector2 topRight = new Vector2(1.0f, 0.0f); //1
            Vector2 bottomRight = new Vector2(1.0f, 1.0f);//3

            //Cube World points (too lazy to write them, taken from a forum)
            Vector3 topLeftBack = cubePosition + new Vector3(-1.0f, 1.0f, -0.22f) * cubeSize;
            Vector3 bottomLeftBack = cubePosition + new Vector3(-1.0f, -0.1f, -0.22f) * cubeSize;
            Vector3 topRightBack = cubePosition + new Vector3(1.0f, 1.0f, -0.22f) * cubeSize;
            Vector3 bottomRightBack = cubePosition + new Vector3(1.0f, -0.1f, -0.22f) * cubeSize;
            Vector3 topLeftFront = cubePosition + new Vector3(-1.0f, 1.0f, 0.28f) * cubeSize;
            Vector3 topRightFront = cubePosition + new Vector3(1.0f, 1.0f, 0.28f) * cubeSize;
            Vector3 bottomLeftFront = cubePosition + new Vector3(-1.0f, -0.1f, 0.28f) * cubeSize;
            Vector3 bottomRightFront = cubePosition + new Vector3(1.0f, -0.1f, 0.28f) * cubeSize;

            //Back of Cube
            cubeVertices[0] = new VertexPositionTexture(topLeftBack, topLeft);
            cubeVertices[1] = new VertexPositionTexture(topRightBack, topRight);
            cubeVertices[2] = new VertexPositionTexture(bottomLeftBack, bottomLeft);
            cubeVertices[3] = new VertexPositionTexture(bottomRightBack, bottomRight);

            //Right Side of the Cube
            cubeVertices[4] = new VertexPositionTexture(bottomRightBack, bottomLeft);
            cubeVertices[5] = new VertexPositionTexture(topRightBack, topLeft);
            cubeVertices[6] = new VertexPositionTexture(bottomRightFront, bottomRight);
            cubeVertices[7] = new VertexPositionTexture(topRightFront, topRight);

            //Top of the Cube
            cubeVertices[8] = new VertexPositionTexture(topRightFront, topRight);
            cubeVertices[10] = new VertexPositionTexture(topLeftFront, topLeft);
            cubeVertices[9] = new VertexPositionTexture(topRightBack, bottomRight);
            cubeVertices[11] = new VertexPositionTexture(topLeftBack, bottomLeft);

            //Left Side of the Cube
            cubeVertices[12] = new VertexPositionTexture(topLeftBack, topLeft);
            cubeVertices[13] = new VertexPositionTexture(bottomLeftBack, bottomLeft);
            cubeVertices[14] = new VertexPositionTexture(topLeftFront, topRight);
            cubeVertices[15] = new VertexPositionTexture(bottomLeftFront, bottomRight);

            //Bottom of the Cube
            cubeVertices[16] = new VertexPositionTexture(bottomLeftFront, topRight);
            cubeVertices[17] = new VertexPositionTexture(bottomLeftBack, bottomRight);
            cubeVertices[18] = new VertexPositionTexture(bottomRightFront, topLeft);
            cubeVertices[19] = new VertexPositionTexture(bottomRightBack, bottomLeft);

            cubeVertices[20] = new VertexPositionTexture(bottomRightFront, topLeft);
            cubeVertices[21] = new VertexPositionTexture(bottomLeftFront, topLeft);
            //Front of Cube
            cubeVertices[22] = new VertexPositionTexture(bottomLeftFront, bottomLeft);
            cubeVertices[23] = new VertexPositionTexture(bottomRightFront, bottomRight);
            cubeVertices[24] = new VertexPositionTexture(topLeftFront, topLeft);
            cubeVertices[25] = new VertexPositionTexture(topRightFront, topRight);

        }
    }
}
