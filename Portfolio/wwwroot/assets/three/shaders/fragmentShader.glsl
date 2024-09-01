uniform sampler2D alphaTexture;
uniform sampler2D elevTexture;

uniform vec3 lowColour;
uniform vec3 highColour;

varying vec2 vUv;
varying float vVisible;

void main() {
    if (floor(vVisible + 0.1) == 0.0) discard;
    float alpha = (1.0 - texture2D(alphaTexture, vUv).r) * 0.3;
    float elevation = texture2D(elevTexture, vUv).r;
    vec3 interpolatedColor = mix(lowColour, highColour, elevation);
    gl_FragColor = vec4(interpolatedColor, alpha);
}
