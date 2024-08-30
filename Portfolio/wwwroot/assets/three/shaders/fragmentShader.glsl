uniform sampler2D colorTexture;
uniform sampler2D alphaTexture;
uniform sampler2D elevTexture;

varying vec2 vUv;
varying float vVisible;

void main() {
    if (floor(vVisible + 0.1) == 0.0) discard;
    float alpha = (1.0 - texture2D(alphaTexture, vUv).r) * 0.3;
    float elevation = texture2D(elevTexture, vUv).r;
    vec3 lowCyan = vec3(0.0, 0.5, 0.5);
    vec3 highCyan = vec3(0.5, 1.0, 1.0);
    vec3 interpolatedColor = mix(lowCyan, highCyan, elevation);
    gl_FragColor = vec4(interpolatedColor, alpha);
}
