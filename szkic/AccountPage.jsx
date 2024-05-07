import axios from 'axios';
import React, { useEffect, useState } from 'react'

export default function AccountPage() {
    const [st, setSt] = useState('Not logged in');

    useEffect(() => {
        const fetchData = async () => {
            const url = 'https://localhost:5001/api/users/' + localStorage.getItem('user');
            const config = {
                headers: {Authorization: 'Bearer ' + localStorage.getItem('jwtToken')}
            }
            try{
                console.log(url)

                const response = await axios.get(url, config);
                setSt(response.data);
            }catch(error){
                console.error(error);
                setSt("Error");
            }
        };
        fetchData();

    }, []);

    return (
        <>
            <div>
                <h1>Account Page</h1>
            </div>
            <div>
                <p>{JSON.stringify(st)}</p>
            </div>
        </>
    )
}
