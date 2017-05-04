#version 330 core
  
layout (location = 0) in vec3 position;
layout (location = 1) in vec3 customColor;
layout (location = 2) in vec2 texCoord;
uniform mat4 transform;
out vec2 TexCoord;

void main()
{
	TexCoord = texCoord;
    gl_Position = transform * vec4(position.x, position.y, position.z, 1.0);
}
