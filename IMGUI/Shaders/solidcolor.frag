#version 330 core
out vec4 color;
uniform vec4 maskColor;

void main()
{
    color = maskColor;
} 