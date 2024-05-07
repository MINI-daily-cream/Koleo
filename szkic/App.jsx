import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
import Login from './Login'
import {useNavigate} from 'react-router-dom'

function App() {
    const navigate = useNavigate();

    function redirect(){
        navigate('/account')
    }
    
    return (
        <>
            <div className='account'>
              <button onClick={redirect}>Account</button>
            </div>
            <Login/>
        </>
    )
}

export default App
