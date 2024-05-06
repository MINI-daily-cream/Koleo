import React, { useState } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faTrain, faCalendar, faClock, faGreaterThan, faMinus, faArrowRight, faUser, faMapMarkerAlt, faTicketAlt } from '@fortawesome/free-solid-svg-icons';
// npm install react-calendar
import Calendar from 'react-calendar';
import 'react-calendar/dist/Calendar.css';
// npm install --save moment react-moment
import moment from 'moment'
import { Link } from 'react-router-dom';
// npm install @mui/material @emotion/react @emotion/styled
import SelectLabels from './sharedComponents/SelectListSourceCity';

const HomePage = () => {
    const [showCalendar, setShowCalendar] = useState(false);
    const [selectedDate, setSelectedDate] = useState(Date.now);
    const [selectedCitySrc, setSelectedCitySrc] = useState('');
    const [selectedCityDst, setSelectedCityDst] = useState('');

    const handleCalendarButtonClick = () => {
        setShowCalendar(!showCalendar);
    }
    const handleDateChange = (date) => {
        setSelectedDate(date);
        setShowCalendar(!showCalendar);
    }
    const handleFindButtonClick = (event) => {
        if(selectedCitySrc == selectedCityDst) {
            alert('Nie można wybrac tego samego miasta');
            event.preventDefault();
            return;     
        }
        window.location.href = "/FoundConnections";
    }
    
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
                        <Link to="/"><button type="submit"
                            className="Button"
                            onClick={handleFindButtonClick }
                            >Wyszukaj</button></Link>
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
        </div>
    )
}

export default HomePage;