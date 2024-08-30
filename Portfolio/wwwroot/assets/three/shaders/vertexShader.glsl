uniform float size;
uniform sampler2D elevTexture;

varying vec2 vUv;
varying float vVisible;

void main() {
    vUv = uv;
    vec4 mvPosition = modelViewMatrix * vec4(position, 1.0);
    float elv = texture2D(elevTexture, vUv).r;
    vec3 vNormal = normalMatrix * normal;
    vVisible = step(0.0, dot( -normalize(mvPosition.xyz), normalize(vNormal)));
    mvPosition.z += 0.35 * elv;
    gl_PointSize = size;
    gl_Position = projectionMatrix * mvPosition;
}