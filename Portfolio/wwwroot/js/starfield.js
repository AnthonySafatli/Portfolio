tsParticles.load("tsparticles", {
	fullScreen: {
		enable: true,
		zIndex: -3,
	},
	particles: {
		number: {
			value: 100,
			density: {
				enable: true,
				area: 800,
			},
		},
		color: { value: "#ffffff" },
		shape: { type: "circle" },
		opacity: {
			value: 1,
			random: true,
			anim: {
				enable: true,
				speed: 1,
				opacity_min: 0.1,
				sync: false,
			},
		},
		size: {
			value: 1.5,
			random: true,
		},
		move: {
			enable: true,
			speed: 0.3,
			direction: "none",
			random: true,
			straight: false,
			outModes: "out",
		},
	},
	interactivity: {
		detectsOn: "canvas",
		events: {
			onhover: { enable: false },
			onclick: { enable: false },
		},
	},
	detectRetina: true,
	background: { color: "#000000" },
});
