function saveAsFile(fileName, byteBase64) {
    var link = document.createElement("a");
    link.download = fileName;
    link.href = "data:application/octet-stream;base64," + byteBase64;
    link.setAttribute('target', '_blank');
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}