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
    const [showListSrc, setShowListSrc] = useState(false);
    const [selectedCitySrc, setSelectedCitySrc] = useState('');
    const [showListDst, setShowListDst] = useState(false);
    const [selectedCityDst, setSelectedCityDst] = useState('');

    const items = ['Item 1', 'Item 2', 'Item 3'];

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
    const handleCitiesListButtonClickScr = (item) => {
        setSelectedCitySrc(item);
        setShowListSrc(!showListSrc);
    }
    const handleCitiesButtonScr = () => {
        setShowListSrc(!showListSrc);
    }
    const handleCitiesListButtonClickDst = (item) => {
        setSelectedCityDst(item);
        setShowListDst(!showListDst);
    }
    const handleCitiesButtonDst = () => {
        setShowListDst(!showListDst);
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
                    {/* <div className="TravelerInfoInput">
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
                    </div> */}
                    <div>
                        <button onClick={handleCitiesButtonScr}>Show List</button>
                        {showListSrc && (
                            <select value={selectedCitySrc} onChange={handleCitiesListButtonClickScr}>
                            {items.map((item, index) => (
                              <option key={index} value={item}>
                                {item}
                              </option>
                            ))}
                          </select>
                        )}
                        {selectedCitySrc && <p>You selected: {selectedCitySrc}</p>}
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