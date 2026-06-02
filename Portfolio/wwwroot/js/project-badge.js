/*!
 * project-badge.js
 * Drop into any project site to show a floating badge linking to your portfolio + GitHub.
 *
 * Usage – add to your HTML before </body>:
 *
 *   <script
 *     src="project-badge.js"
 *     data-portfolio="https://yourportfolio.com/projects/my-project"
 *     data-github="https://github.com/you/my-project"
 *     data-label="My Project"        <!-- optional, shown in popup -->
 *     data-position="bottom-right"   <!-- bottom-right (default) | bottom-left | top-right | top-left -->
 *     data-size="52px"               <!-- size of the icon -->
 *   ></script>
 */

(function () {
	const script =
		document.currentScript ||
		document.querySelector("script[data-portfolio], script[data-github]");

	const portfolioUrl = script?.dataset.portfolio || "#";
	const githubUrl = script?.dataset.github || "#";
	const label = script?.dataset.label || "Project";
	const position = script?.dataset.position || "bottom-right";
	const size = script?.dataset.size || "52px";

	/* ── CSS ────────────────────────────────────────────────────────────── */
	const css = `
    #pb-badge {
      --pb-size: ${size};
      --pb-gap: 16px;
      --pb-bg: #0a0a0a;
      --pb-border: rgba(126, 232, 232, 0.15);
      --pb-cyan: #0cacbe;
      --pb-cyan-light: #7ee8e8;
      --pb-pink: #f472b6;
      --pb-pink-light: #fbb6d4;
      --pb-text: #c2f4f4;
      --pb-muted: rgba(194, 244, 244, 0.45);
      --pb-radius: 4px;
      --pb-shadow: 0 8px 32px rgba(0,0,0,0.45);
      --pb-glow: 0 0 6px rgba(126,232,232,0.35), 0 0 14px rgba(126,232,232,0.35);
      --pb-glow-pink: 0 0 8px rgba(244,114,182,0.5), 0 0 20px rgba(244,114,182,0.3);
      --pb-transition: 0.22s cubic-bezier(0.34,1.56,0.64,1);

      position: fixed;
      z-index: 2147483647;
      font-family: 'DM Mono', 'SF Mono', monospace;
      font-size: 12px;
    }

    /* ── positioning ── */
    #pb-badge[data-pos="bottom-right"] { bottom: var(--pb-gap); right: var(--pb-gap); }
    #pb-badge[data-pos="bottom-left"]  { bottom: var(--pb-gap); left:  var(--pb-gap); }
    #pb-badge[data-pos="top-right"]    { top:    var(--pb-gap); right: var(--pb-gap); }
    #pb-badge[data-pos="top-left"]     { top:    var(--pb-gap); left:  var(--pb-gap); }

    /* ── trigger button ── */
    #pb-trigger {
      width: var(--pb-size);
      height: var(--pb-size);
      border-radius: 50%;
      background: var(--pb-bg);
      border: 1px solid var(--pb-border);
      box-shadow: var(--pb-shadow);
      cursor: pointer;
      display: flex;
      align-items: center;
      justify-content: center;
      color: var(--pb-cyan-light);
      transition: transform 0.18s ease, box-shadow 0.18s ease;
      user-select: none;
      -webkit-user-select: none;
    }
    @property --pb-angle {
      syntax: '<angle>';
      initial-value: 0deg;
      inherits: false;
    }
    @keyframes pb-spin {
      to { --pb-angle: 360deg; }
    }
    #pb-trigger:hover {
      transform: scale(1.10);
      border-color: transparent;
      animation: pb-spin 2s linear infinite;
      background:
        linear-gradient(var(--pb-bg), var(--pb-bg)) padding-box,
        conic-gradient(from var(--pb-angle), #0cacbe, #7ee8e8, #f472b6, #c084a0, #0cacbe) border-box;
    }
    #pb-trigger svg { pointer-events: none; }

    /* ── panel ── */
    #pb-panel {
      position: absolute;
      background: var(--pb-bg);
      border: 1px solid var(--pb-border);
      border-radius: var(--pb-radius);
      box-shadow: var(--pb-shadow);
      padding: 12px 10px 10px;
      min-width: 148px;
      display: flex;
      flex-direction: column;
      gap: 6px;

      opacity: 0;
      pointer-events: none;
      transform: scale(0.88) translateY(6px);
      transform-origin: bottom center;
      transition:
        opacity 0.18s ease,
        transform var(--pb-transition);
    }

    /* panel position offset */
    #pb-badge[data-pos^="bottom"] #pb-panel { bottom: calc(var(--pb-size) + 10px); }
    #pb-badge[data-pos^="top"]    #pb-panel { top:    calc(var(--pb-size) + 10px); }
    #pb-badge[data-pos$="right"]  #pb-panel { right: 0; transform-origin: bottom right; }
    #pb-badge[data-pos$="left"]   #pb-panel { left:  0; transform-origin: bottom left; }
    #pb-badge[data-pos^="top"]    #pb-panel { transform-origin: top center; transform: scale(0.88) translateY(-6px); }

    #pb-badge.pb-open #pb-panel {
      opacity: 1;
      pointer-events: auto;
      transform: scale(1) translateY(0);
    }

    /* label */
    #pb-label {
      color: var(--pb-muted);
      font-size: 10px;
      letter-spacing: 0.08em;
      text-transform: uppercase;
      padding: 0 4px 2px;
      border-bottom: 1px solid rgba(126, 232, 232, 0.12);
      margin-bottom: 2px;
      white-space: nowrap;
      overflow: hidden;
      text-overflow: ellipsis;
    }

    /* links */
    .pb-link {
      display: flex;
      align-items: center;
      gap: 9px;
      padding: 7px 8px;
      border-radius: var(--pb-radius);
      color: var(--pb-cyan-light);
      text-decoration: none;
      transition: background 0.12s ease, color 0.12s ease, text-shadow 0.12s ease;
    }
    .pb-link:hover {
      background: rgba(126, 232, 232, 0.07);
      color: #c2f4f4;
      text-shadow: var(--pb-glow);
    }
    .pb-link:hover svg { color: var(--pb-cyan-light); }
    .pb-link svg {
      flex-shrink: 0;
      transition: color 0.12s ease;
      color: var(--pb-pink-light);
    }
    .pb-link span { letter-spacing: 0.02em; }
  `;

	/* ── SVG icons ──────────────────────────────────────────────────────── */
	const iconTrigger = `<svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.8" stroke-linecap="round" stroke-linejoin="round">
    <circle cx="12" cy="12" r="10"/>
    <line x1="12" y1="8" x2="12" y2="12"/>
    <line x1="12" y1="16" x2="12.01" y2="16"/>
  </svg>`;

	const iconPortfolio = `<svg width="15" height="15" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.8" stroke-linecap="round" stroke-linejoin="round">
    <rect x="2" y="7" width="20" height="14" rx="2"/>
    <path d="M16 7V5a2 2 0 0 0-2-2h-4a2 2 0 0 0-2 2v2"/>
  </svg>`;

	const iconGitHub = `<svg width="15" height="15" viewBox="0 0 24 24" fill="currentColor">
    <path d="M12 2C6.48 2 2 6.58 2 12.21c0 4.5 2.87 8.32 6.84 9.67.5.09.68-.22.68-.49v-1.71c-2.78.62-3.37-1.37-3.37-1.37-.46-1.19-1.12-1.51-1.12-1.51-.91-.64.07-.63.07-.63 1.01.07 1.54 1.06 1.54 1.06.9 1.57 2.36 1.12 2.93.86.09-.66.35-1.12.64-1.38-2.22-.26-4.56-1.14-4.56-5.06 0-1.12.39-2.03 1.03-2.75-.1-.26-.45-1.3.1-2.71 0 0 .84-.27 2.75 1.05A9.4 9.4 0 0 1 12 7.9c.85 0 1.7.12 2.5.34 1.91-1.32 2.75-1.05 2.75-1.05.55 1.41.2 2.45.1 2.71.64.72 1.03 1.63 1.03 2.75 0 3.93-2.34 4.8-4.57 5.05.36.32.68.94.68 1.9v2.82c0 .27.18.59.69.49C19.13 20.53 22 16.71 22 12.21 22 6.58 17.52 2 12 2z"/>
  </svg>`;

	/* ── DOM ────────────────────────────────────────────────────────────── */
	function build() {
		// inject styles
		const style = document.createElement("style");
		style.textContent = css;
		document.head.appendChild(style);

		// wrapper
		const badge = document.createElement("div");
		badge.id = "pb-badge";
		badge.setAttribute("data-pos", position);

		// trigger
		const trigger = document.createElement("button");
		trigger.id = "pb-trigger";
		trigger.setAttribute("aria-label", "Project info");
		trigger.innerHTML = iconTrigger;

		// panel
		const panel = document.createElement("div");
		panel.id = "pb-panel";
		panel.setAttribute("role", "dialog");
		panel.setAttribute("aria-label", "Project links");

		const lbl = document.createElement("div");
		lbl.id = "pb-label";
		lbl.textContent = label;

		const linkPortfolio = document.createElement("a");
		linkPortfolio.className = "pb-link";
		linkPortfolio.href = portfolioUrl;
		linkPortfolio.target = "_blank";
		linkPortfolio.rel = "noopener noreferrer";
		linkPortfolio.innerHTML = `${iconPortfolio}<span>Portfolio</span>`;

		const linkGithub = document.createElement("a");
		linkGithub.className = "pb-link";
		linkGithub.href = githubUrl;
		linkGithub.target = "_blank";
		linkGithub.rel = "noopener noreferrer";
		linkGithub.innerHTML = `${iconGitHub}<span>GitHub</span>`;

		panel.appendChild(lbl);
		panel.appendChild(linkPortfolio);
		panel.appendChild(linkGithub);

		badge.appendChild(trigger);
		badge.appendChild(panel);
		document.body.appendChild(badge);

		/* ── behaviour ── */
		trigger.addEventListener("click", (e) => {
			e.stopPropagation();
			badge.classList.toggle("pb-open");
		});

		document.addEventListener("click", (e) => {
			if (!badge.contains(e.target)) badge.classList.remove("pb-open");
		});

		document.addEventListener("keydown", (e) => {
			if (e.key === "Escape") badge.classList.remove("pb-open");
		});
	}

	if (document.readyState === "loading") {
		document.addEventListener("DOMContentLoaded", build);
	} else {
		build();
	}
})();
