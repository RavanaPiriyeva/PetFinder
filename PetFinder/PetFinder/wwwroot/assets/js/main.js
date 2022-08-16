var breed = document.querySelector(".breeds")
var resource = document.querySelector(".resource")
var breedMenu = document.querySelector(".breed-menu")
var resourceMenu = document.querySelector(".resource-menu")
// var heroCards = document.querySelector(".hero-cards")
// let x= heroCards.scrollHeight
// console.log(x)
function openBreedMenu() {
    if (breedMenu.style.display == "block") {
        breedMenu.style.display = "none"
        breed.querySelector('i').classList.remove("fa-angle-up")
        breed.querySelector('i').classList.add("fa-angle-down")

    }
    else {
        breedMenu.style.display = "block"
        if (resourceMenu.style.display == "block") {
            resource.querySelector('i').classList.remove("fa-angle-up")
            resource.querySelector('i').classList.add("fa-angle-down")
            resourceMenu.style.display = "none"
        }
        breed.querySelector('i').classList.remove("fa-angle-down")
        breed.querySelector('i').classList.add("fa-angle-up")

    }
}


function openResourceMenu() {
    if (resourceMenu.style.display == "block") {
        resourceMenu.style.display = "none"
        resource.querySelector('i').classList.remove("fa-angle-up")
        resource.querySelector('i').classList.add("fa-angle-down")
    }
    else {
        resourceMenu.style.display = "block"
        if (breedMenu.style.display == "block") {
            breed.querySelector('i').classList.remove("fa-angle-up")
            breed.querySelector('i').classList.add("fa-angle-down")
            breedMenu.style.display = "none"
        }
        resource.querySelector('i').classList.remove("fa-angle-down")
        resource.querySelector('i').classList.add("fa-angle-up")
    }
}
breed.onclick = function () { openBreedMenu() };
resource.onclick = function () { openResourceMenu() }