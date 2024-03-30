// Hover Selecting
let dateSelectors = document.querySelectorAll(".week-selectors p");

let weekGroups = [
    document.querySelectorAll(".birth"),

    // Education
    document.querySelectorAll(".elem"),
    document.querySelectorAll(".junior"),
    document.querySelectorAll(".high"),
    document.querySelectorAll(".dal"),

    // Career
    document.querySelectorAll(".jakes"),
    document.querySelectorAll(".medit"),
];

dateSelectors.forEach((selector, index) => {
    selector.addEventListener("mouseenter", () => {
        weekGroups[index].forEach((weeks) => {
            weeks.classList.add("selected-week");
        });
    });

    selector.addEventListener("mouseleave", () => {
        weekGroups[index].forEach((weeks) => {
            weeks.classList.remove("selected-week");
        });
    })
});