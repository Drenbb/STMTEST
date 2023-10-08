function goToRootDir(path) {
    console.log(decodeURI(path));
    debugger
    // const buttonElements = document.getElementById('btnGoToRoot');
    // const rootPath = buttonElements.getAttribute('data-id');
    fetch('/Dirictory/Index', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(decodeURI(path))
    })
        .then(response => response.text())
        .then(data => {
            document.getElementById('frame').innerHTML = data;
        });
}

// document.getElementById('btnGoToRoot').addEventListener('click', goToRoot);
