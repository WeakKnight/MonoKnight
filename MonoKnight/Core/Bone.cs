using System;
using OpenTK;
using System.Collections.Generic;

namespace MonoKnight
{
    public struct VertexWeight
    {
        public int vertexId;
        public float weight;
    }
    
    public class Bone:Object
    {
        public Bone()
        {
        }

        public Matrix4 offset = Matrix4.Identity; 
        public string name = "";
        public List<VertexWeight> weightList = new List<VertexWeight>();
    }
}
