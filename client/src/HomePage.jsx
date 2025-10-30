import React, { useState, useEffect } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faTrain, faCalendar, faClock, faGreaterThan, faMinus, faArrowRight, faUser, faMapMarkerAlt, faTicketAlt } from '@fortawesome/free-solid-svg-icons';
// npm install react-calendar
import Calendar from 'react-calendar';
import 'react-calendar/dist/Calendar.css';
// npm install --save moment react-moment
import moment from 'moment'
import { Link, useNavigate } from 'react-router-dom';
// npm install @mui/material @emotion/react @emotion/styled
import SelectLabels from './sharedComponents/SelectListSourceCity';
import AdvertismentList from './AdvertimsmentFiles/AdvertismentList';
import apiBaseUrl from './config';

const HomePage = () => {
    const navigate = useNavigate();
    const [showCalendar, setShowCalendar] = useState(false);
    const [selectedDate, setSelectedDate] = useState(Date.now);
    const [selectedCitySrc, setSelectedCitySrc] = useState('');
    const [selectedCityDst, setSelectedCityDst] = useState('');
    const [ads, setAds] = useState([])
    
    const handleCalendarButtonClick = () => {
        setShowCalendar(!showCalendar);
    }
    const handleDateChange = (date) => {
        setSelectedDate(date);
        setShowCalendar(!showCalendar);
    }
    const handleFindButtonClick = (event) => {
        if(selectedCitySrc == selectedCityDst) {
            alert('Nie można wybrać tego samego miasta');
            event.preventDefault();
            return;     
        }

        // if(new Date(selectedDate).getDate() < new Date(Date.now()).getDate()) {
        //     console.log(new Date(selectedDate).getDate());
        //     console.log(new Date(Date.now()).getDate());
        //     alert('Nie można wybrać daty z przeszłości');
        //     event.preventDefault();
        //     return;     
        // }
        
        console.log(selectedDate);
        console.log(new Date(selectedDate));
        console.log((new Date(selectedDate)).toString());

        navigate("/FoundConnections", {state: {
            startCity: selectedCitySrc,
            endCity: selectedCityDst,
            // day: "2024-05-16T17:08:37.872Z"
            // day: (new Date(selectedDate)).toString()
            day: (new Date(selectedDate)).toISOString()
        }});
    }
    

    function getAllAds(){
        const xhr = new XMLHttpRequest();
        xhr.onreadystatechange = function () {
            if (xhr.readyState === XMLHttpRequest.DONE) {
                if (xhr.status === 200) {
                    const response = JSON.parse(xhr.responseText);
                    //const response = xhr.responseText;
                    console.log(response);
                    setAds(response);
                } else {
                    console.error('Błąd pobierania danych:', xhr.status);
                    // Obsługa błędów
                }
            }
        };

        // xhr.open('GET', `https://localhost:5001/api/Ad/GetAllAds`);
        xhr.open('GET', `${apiBaseUrl}/api/Ad/GetAllAds`);
        xhr.send();
    }

    useEffect( () => {
        getAllAds();
    }, [])
    return (
        <div>
            <div className="TicketInfoHeader">
                <p>Witamy w Koleo</p>
            </div>
            <div className='account-panel'>
                <h1>Wyszukaj polączenie</h1>
                <div className='account-panel-inside1'>
                    <div className='ConnectionInfoColumn'>
                    <div>
                       <SelectLabels onCityChange={setSelectedCitySrc} LabelName={"Skąd"}></SelectLabels>
                       {/* <p>Selected City: {selectedCitySrc}</p>  */}
                    </div>
                    <div>
                       <SelectLabels onCityChange={setSelectedCityDst} LabelName={"Dokąd"}></SelectLabels>
                       {/* <p>Selected City: {selectedCityDst}</p> */}
                    </div>
                    <div className='dateViewTile'>
                        <button className='icon-button' onClick={handleCalendarButtonClick}>
                            <FontAwesomeIcon icon={faCalendar} />
                        </button>
                            <div className='text'>{moment(selectedDate).format('MMMM Do YYYY')}</div>
                    </div>
                    <div className="ButtonAligment">
                        {/*TODO: set "to" prop*/}
                        {/* <Link to="/"> */}
                            <button type="button"
                            className="Button"
                            onClick={handleFindButtonClick }
                            >Wyszukaj</button>
                        {/* </Link> */}
                    </div>
                    </div>
                    <div className='calendarButton'>
                        {/* <button onClick={handleCalendarButtonClick}>Select Date</button> */}
                        {showCalendar && (
                            <div>
                                <Calendar
                                onChange={handleDateChange}
                                value={selectedDate}
                                />
                            </div>
                        )}
                    </div>
                </div>
                
            </div>
            <div className='AdList'>
                <AdvertismentList ads={ads} />
            </div>
        </div>
    )
}

export default HomePage;