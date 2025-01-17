#version 460 core

in vec3 FragPos;
in vec3 Normal;
in vec2 TexCoords;

out vec4 FragColor;

uniform sampler2D image;       
uniform vec3 lightPos;         
uniform vec3 viewPos;          
uniform vec3 lightColor;       
uniform float ambientStrength; 
uniform float specularStrength;


uniform vec3 objectColor;     

void main()
{
    // 1. Ambient
    vec3 ambient = ambientStrength * lightColor;

    // 2. Diffuse
    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(lightPos - FragPos);
    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = diff * lightColor;

    // 3. Specular
    vec3 viewDir = normalize(viewPos - FragPos);
    vec3 reflectDir = reflect(-lightDir, norm);
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), 32.0); 
    vec3 specular = specularStrength * spec * lightColor;

    vec3 lighting = ambient + diffuse + specular;

    vec3 baseColor = texture(image, TexCoords).rgb;

    baseColor *= objectColor;

    FragColor = vec4(lighting * baseColor, 1.0);
}
