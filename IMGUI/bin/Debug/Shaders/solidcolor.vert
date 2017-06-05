#version 330 core
layout (location = 0) in vec4 vertex;
uniform mat4 model;
uniform mat4 projection;

void main()
{
     vec4 pos = projection * model * vec4(vertex.xy, -3.0, 1.0);
     gl_Position = vec4(pos.x, - pos.y, pos.z, pos.w);
}
