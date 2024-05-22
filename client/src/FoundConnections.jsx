import { useEffect, useState } from 'react'
import ConnectionList from './ConnectionFiles/ConnectionList';
import apiBaseUrl from './config';
import axios from 'axios';
import { useLocation } from 'react-router-dom';

const FoundConnectionList = () => {
  const { state } = useLocation();
  // const [filters, setFilters] = useState({});
  const [connections, setConnections] = useState([])
  const [jwtToken, setJwtToken] = useState(localStorage.getItem('jwtToken'));

  function getConnections() {
    const fetchData = async () => {
      // const requestBody = {
      //   startCity: "Warszawa",
      //   endCity: "Łódź",
      //   day: "2024-05-16T17:08:37.872Z"
      // };
    console.log("state")
    console.log(state)
      try {
        const response = await axios.post(`${apiBaseUrl}/api/Connection/filtered`, state, {
            headers: {
                'Content-Type': 'application/json',
                // 'Authorization': `Bearer ${jwtToken}`
            }
        });
        console.log('moj pies to suka');
        console.log(response.data);
        setConnections(response.data);
        console.log("fetched connections")
        console.log(connections)
      } 
      catch(error) {
        if (error === 'Bad request') {
            console.error('user exists');
        } else {
            console.error('An error occurred:', error);
        }
      }
    };
    fetchData();
  }

  useEffect( () => {
    console.log("state")
    console.log(state)
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
