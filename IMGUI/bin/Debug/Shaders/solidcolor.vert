#version 330 core
layout (location = 0) in vec3 position;

out vec2 TexCoord;

void main()
{
    gl_Position = position;
}
