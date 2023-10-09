function goToRootDir(path,orderBy) {
    console.log(decodeURI(path));
    const findDirectoryDto = {
        path: decodeURI(path),
        orderByDesc: orderBy
    };
    fetch('/Directory/Index', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(findDirectoryDto)
    })
        .then(response => response.text())
        .then(data => {
            document.getElementById('frame').innerHTML = data;
        });
}
