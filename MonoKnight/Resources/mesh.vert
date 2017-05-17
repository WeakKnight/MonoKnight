#version 330 core
  
layout (location = 0) in vec3 position;
layout (location = 1) in vec3 normal;

uniform mat4 project;

void main()
{
    gl_Position = project * vec4(position.x, position.y - 2.5 , position.z, 1.0);
}
