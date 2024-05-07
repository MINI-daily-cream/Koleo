import React, { useState } from 'react'
import axios from 'axios'
export default function Login() {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [content, setContent] = useState(null)

    function onSubmit(){
        const fetchData = async () => {
            try{
                const response = await axios.post('http://localhost:5001/api/account/login',{
                    "username": username,
                    "password": password
                });
                setContent(response.data);
                localStorage.setItem('jwtToken', response.data.token);
                localStorage.setItem('user', response.data.username);
                // localStorage.setItem('user', username);
            }catch(error){
                console.error('Error fetching data: ', error);
                setContent('Wrong password')
            }
        };
        fetchData();
    }

    return (
        <>
            <h1>Login</h1>
            <div>
                <input placeholder='Username' onChange={(x) => {setUsername(x.target.value); console.log(username)}}></input>
            </div>
            <div>
                <input placeholder='Password' onChange={(x) => {setPassword(x.target.value); console.log(password)}}></input>
            </div>
            <div>
                <button onClick={onSubmit}>Submit</button>
            </div>
            <div>
                Username: {username}
            </div>
            <div>
                Password: {password}
            </div>
            <div>
                {content ? <pre>{JSON.stringify(content, null, 2)}</pre> : <p>Loading...</p>}
            </div>
        </>
    )
}
