var image = document.getElementById("preview");
var file = document.getElementById("picture");

file.addEventListener('change', function () {
    image.src = URL.createObjectURL(file.files[0]);
    image.style.display = "block";
    image.style.cssText = "width: 120px; height: 120px";
});