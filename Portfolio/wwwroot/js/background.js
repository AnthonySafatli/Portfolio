import * as THREE from "three";
import { OrbitControls } from "three/examples/jsm/controls/OrbitControls.js";
import getStarfield from "./getStarfield.js";

// Error Checking
if (!(typeof globePos !== 'undefined')) {
    throw new Error("Error: Globe position undefined!");
}
if (!(typeof lowColour !== 'undefined' || typeof highColour !== 'undefined' || typeof wireColour !== 'undefined')) {
    throw new Error("Error: Globe colour undefined!");
}
if (!(typeof globeOpacity !== 'undefined')) {
    throw new Error("Error: Globe opacity undefined!");
}

// Shaders
import vertexShader from "../assets/three/shaders/vertexShader.glsl";
import fragmentShader from "../assets/three/shaders/fragmentShader.glsl";

// three.js Setup
const scene = new THREE.Scene();
const camera = new THREE.PerspectiveCamera(45, innerWidth / innerHeight, 0.1, 1000);
camera.position.set(0, 0, 0);
const canvas = document.querySelector(".webgl");
const renderer = new THREE.WebGLRenderer({ canvas, antialias: true });
renderer.setSize(innerWidth, innerHeight);
renderer.setPixelRatio(window.devicePixelRatio);

// Textures
const textureLoader = new THREE.TextureLoader();
const starSprite = textureLoader.load("/assets/three/circle.png");
const elevMap = textureLoader.load("/assets/three/01_earthbump1k.jpg");
const alphaMap = textureLoader.load("/assets/three/02_earthspec1k.jpg");

// Icosphere Geometry
const globeGroup = new THREE.Group();
scene.add(globeGroup);

const geo = new THREE.IcosahedronGeometry(1, 10);
const mat = new THREE.MeshBasicMaterial({
    color: wireColour,
    opacity: globeOpacity,
    wireframe: true,
    transparent: true,
});
const cube = new THREE.Mesh(geo, mat);
globeGroup.add(cube);

// Earth Geometry
const detail = 110;
const pointsGeo = new THREE.IcosahedronGeometry(1, detail);

const uniforms = {
    opacity: { type: "f", value: globeOpacity * 0.3 },
    size: { type: "f", value: 4.0 },
    elevTexture: { type: "t", value: elevMap },
    alphaTexture: { type: "t", value: alphaMap },
    lowColour: { value: lowColour },
    highColour: { value: highColour },
};

const pointsMat = new THREE.ShaderMaterial({
    uniforms: uniforms,
    vertexShader,
    fragmentShader,
    transparent: true
});

const points = new THREE.Points(pointsGeo, pointsMat);
globeGroup.add(points);

globeGroup.position.set(globePos[0], globePos[1], globePos[2]); 

// Lighting
const hemiLight = new THREE.HemisphereLight(0xffffff, 0x080820, 3);
scene.add(hemiLight);

// Stars
const stars = getStarfield({ numStars: 1000, sprite: starSprite });
scene.add(stars);

// Animation
function animate() {
    renderer.render(scene, camera);
    globeGroup.rotation.y += 0.0015;

    requestAnimationFrame(animate);
};
animate();

// Resizing
window.addEventListener('resize', function () {
    camera.aspect = window.innerWidth / window.innerHeight;
    camera.updateProjectionMatrix();
    renderer.setSize(window.innerWidth, window.innerHeight);
}, false);
