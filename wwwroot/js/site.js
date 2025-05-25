// wwwroot/js/site.js
import * as THREE from 'https://cdn.jsdelivr.net/npm/three@0.128.0/build/three.module.js';

// Initialize 3D background animation on page load
document.addEventListener('DOMContentLoaded', () => {
    // Constants for animation
    const PARTICLE_COUNT = 500;
    const MAX_DISTANCE = 50;
    const GLOW_COLOR = 0x007bff;
    const ANIMATION_SPEED = 0.01;

    // Scene setup
    const scene = new THREE.Scene();
    const camera = new THREE.PerspectiveCamera(75, window.innerWidth / window.innerHeight, 0.1, 1000);
    const renderer = new THREE.WebGLRenderer({ alpha: true });
    renderer.setSize(window.innerWidth, window.innerHeight);
    const container = document.getElementById('threejs-background');
    container.appendChild(renderer.domElement);

    // Ambient light for subtle illumination
    const ambientLight = new THREE.AmbientLight(0x404040);
    scene.add(ambientLight);

    // Point light for glowing effect
    const pointLight = new THREE.PointLight(0xffffff, 1, 100);
    pointLight.position.set(10, 10, 10);
    scene.add(pointLight);

    // Particle system
    const particles = [];
    const geometry = new THREE.SphereGeometry(0.2, 16, 16);
    const material = new THREE.MeshStandardMaterial({
        color: GLOW_COLOR,
        emissive: GLOW_COLOR,
        emissiveIntensity: 0.5,
        roughness: 0.5,
        metalness: 0.2
    });

    // Create particles with random positions and velocities
    for (let i = 0; i < PARTICLE_COUNT; i++) {
        const particle = new THREE.Mesh(geometry, material);
        particle.position.set(
            (Math.random() - 0.5) * MAX_DISTANCE,
            (Math.random() - 0.5) * MAX_DISTANCE,
            (Math.random() - 0.5) * MAX_DISTANCE
        );
        particle.velocity = {
            x: (Math.random() - 0.5) * ANIMATION_SPEED,
            y: (Math.random() - 0.5) * ANIMATION_SPEED,
            z: (Math.random() - 0.5) * ANIMATION_SPEED
        };
        scene.add(particle);
        particles.push(particle);
    }

    camera.position.z = 30;

    // Mouse interaction variables
    let mouseX = 0;
    let mouseY = 0;

    // Update mouse position
    document.addEventListener('mousemove', (event) => {
        mouseX = (event.clientX / window.innerWidth) * 2 - 1;
        mouseY = -(event.clientY / window.innerHeight) * 2 + 1;
    });

    // Animation loop
    function animate() {
        requestAnimationFrame(animate);

        // Animate particles
        particles.forEach(particle => {
            // Sinusoidal orbit
            particle.position.x += Math.sin(Date.now() * 0.001 + particle.position.y) * 0.02 + particle.velocity.x;
            particle.position.y += Math.cos(Date.now() * 0.001 + particle.position.z) * 0.02 + particle.velocity.y;
            particle.position.z += Math.sin(Date.now() * 0.001 + particle.position.x) * 0.02 + particle.velocity.z;

            // Mouse interaction
            particle.position.x += mouseX * 0.05;
            particle.position.y += mouseY * 0.05;

            // Keep particles within bounds
            if (Math.abs(particle.position.x) > MAX_DISTANCE) particle.velocity.x *= -1;
            if (Math.abs(particle.position.y) > MAX_DISTANCE) particle.velocity.y *= -1;
            if (Math.abs(particle.position.z) > MAX_DISTANCE) particle.velocity.z *= -1;
        });

        // Update camera for subtle parallax
        camera.position.x += (mouseX * 5 - camera.position.x) * 0.05;
        camera.position.y += (-mouseY * 5 - camera.position.y) * 0.05;
        camera.lookAt(scene.position);

        renderer.render(scene, camera);
    }
    animate();

    // Handle window resize
    window.addEventListener('resize', () => {
        camera.aspect = window.innerWidth / window.innerHeight;
        camera.updateProjectionMatrix();
        renderer.setSize(window.innerWidth, window.innerHeight);
    });
});