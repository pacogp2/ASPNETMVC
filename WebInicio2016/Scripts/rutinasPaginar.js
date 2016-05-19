function mostrarSimbolo() {
    var campo = document.getElementById("hdfCampo").value;
    var simbolo = document.getElementById("hdfSimbolo").value;
    var span = document.getElementById("spn" + campo);
    span.innerHTML = simbolo;
}