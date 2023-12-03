import { useEffect, useState } from 'react';
import React from "react";
import './App.css';

function App() {
    const [files, setFiles] = useState();

    useEffect(() => {
        loadFileList();
    }, []);

    const contents = files === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. </em></p>
        : <table className="table table-striped" aria-labelledby="tabelLabel">
            <thead>
                <tr>
                    <th>Name</th>
                    <th>lastModify</th>
                    <th>status</th>
                </tr>
            </thead>
            <tbody>
                {files.map(file =>
                    <tr key={file.id}>
                        <td>{file.name}</td>
                        <td>{file.modify}</td>
                        <td>
                            {file.state == 0 && 'upload'}
                            {file.state == 1 && 'in process'}
                            {file.state == 2 && <a href={file.url}>ready</a>}
                            {file.state == 3 && 'error'}
                        </td>
                    </tr>
                )}
            </tbody>
        </table>;

    return (
        <div>
            <h1 id="tabelLabel">File list</h1>
            <form onSubmit={onSubmit}>
                <input type='file' id='customFile' accept=".html" />
                <button type="submit">Upload</button>
            </form>
            <hr/>
            {contents}
        </div>
    );
    async function loadFileList() {
        const response = await fetch('File/list/');
        const data = await response.json();
        setFiles(data);
        
    }

    function onSubmit() {
        var fileInput = document.getElementById("customFile");
        var formData = new FormData();
        formData.append('file', fileInput.files[0]);
        fetch('File/upload', {

            method: 'POST',
            mode: 'cors',
            body: formData

        })
    }

}

export default App;