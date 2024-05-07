import { useEffect, useState } from 'react'
import ConnectionList from './ConnectionFiles/ConnectionList';
import apiBaseUrl from './config';

const FoundConnectionList = () => {
  //const [userId, setuserId] = useState("C4630E12-DEE8-411E-AF44-E3CA970455CE")
  const [connections, setConnections] = useState([])

  function getConnections(){
    const xhr = new XMLHttpRequest();
    xhr.onreadystatechange = function () {
        if (xhr.readyState === XMLHttpRequest.DONE) {
            if (xhr.status === 200) {
                const response = JSON.parse(xhr.responseText);
                //const response = xhr.responseText;
                // console.log(response);
                setConnections(response);
            } else {
                console.error('Błąd pobierania danych:', xhr.status);
                // Obsługa błędów
            }
        }
    };

    // xhr.open('GET', `https://localhost:5001/api/Connection`);
    xhr.open('GET', `${apiBaseUrl}/api/Connection`);
    xhr.send();
  }

  useEffect( () => {
    getConnections();
  }, [])

  return (
    <div className='account-panel'>
        <h1>Wybierz polączenie</h1>
        <div className='account-panel-inside'>
            <ConnectionList connections={connections} />
        </div>
    </div>
  );
};

export default FoundConnectionList;
