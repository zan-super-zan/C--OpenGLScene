﻿#version 460 core
layout(location = 0) in vec3 aPos;

out vec3 TexCoords;

uniform mat4 View;
uniform mat4 Proj;

void main()
{
    TexCoords = aPos;
    vec4 pos = Proj * mat4(mat3(View)) * vec4(aPos, 1.0); 
    gl_Position = pos.xyww;
}
