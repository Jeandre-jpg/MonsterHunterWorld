var quadimages = document.querySelectorAll("#quad figure");
for (i = 0; i < quadimages.length; i++) {
    quadimages[i].addEventListener('click', function () { this.classList.toggle("expanded"); quad.classList.toggle("full") });
}


function myFunction() {
    var x = document.getElementById("myDIV");
    if (x.style.display === "none") {
        x.style.display = "block";
    } else {
        x.style.display = "none";
    }


