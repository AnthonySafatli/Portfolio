// Tab View
let tabs = document.querySelectorAll(".tabs h2");
let tabContents = document.querySelectorAll(".tab-content div");

tabs.forEach((tab, index) => {
    tab.addEventListener("click", () => {
        tabContents.forEach((content) => {
            content.classList.remove("active");
        });

        tabs.forEach((tab) => {
            tab.classList.remove("active");
        });

        tabContents[index].classList.add("active");
        tabs[index].classList.add("active");
    });
});

// Hover Selecting
let dateSelectors = document.querySelectorAll(".tab-content p");

let weekGroups = [
    document.querySelectorAll(".elem"),
    document.querySelectorAll(".junior"),
    document.querySelectorAll(".high"),
    document.querySelectorAll(".dal"),

    document.querySelectorAll(".jakes"),
    document.querySelectorAll(".medit"),

    document.querySelectorAll(".birth"),
    document.querySelectorAll(".code"),
    document.querySelectorAll(".hgrad"),
    document.querySelectorAll(".ugrad")
];

dateSelectors.forEach((selector, index) => {
    selector.addEventListener("mouseenter", () => {
        weekGroups[index].forEach((weeks) => {
            console.log("Hover over");
            weeks.classList.add("selected-week");
        });
    });

    selector.addEventListener("mouseleave", () => {
        weekGroups[index].forEach((weeks) => {
            console.log("not hover");
            weeks.classList.remove("selected-week");
        });
    })
});