import React, { useState } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faTrain, faCalendar, faClock, faGreaterThan, faMinus, faArrowRight, faUser, faMapMarkerAlt, faTicketAlt } from '@fortawesome/free-solid-svg-icons';
// npm install react-calendar
import Calendar from 'react-calendar';
import 'react-calendar/dist/Calendar.css';
// npm install --save moment react-moment
import moment from 'moment'
import { Link } from 'react-router-dom';

const LoginPage = () => {
    const [sourceCity, setSourceCity] = useState('');
    const [destinationCity, setDestinationCity] = useState('');
    const [showCalendar, setShowCalendar] = useState(false);
    const [selectedDate, setSelectedDate] = useState(Date.now);

    const handleSourceCityChange = (e) => {
        setSourceCity(e.target.value);
    };
    const handleDestinationCityChange = (e) => {
        setDestinationCity(e.target.value);
    };
    const handleCalendarButtonClick = () => {
        setShowCalendar(!showCalendar);
    }
    const handleDateChange = (date) => {
        setSelectedDate(date);
        setShowCalendar(!showCalendar);
    }
    const handleFindButtonClick = () => {

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
                    <div className="TravelerInfoInput">
                        <label htmlFor="name">Skąd:</label>
                        <input
                            type="text"
                            placeholder='Warszawa'
                            id="sourceCity"
                            value={sourceCity}
                            onChange={handleSourceCityChange}
                            required
                        />
                    </div>
                    <div className="TravelerInfoInput">
                        <label htmlFor="surname">Dokąd:</label>
                        <input
                            type="text"
                            placeholder='Kraków'
                            id="destinationCity"
                            value={destinationCity}
                            onChange={handleDestinationCityChange} 
                            required
                        />
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

export default LoginPage;