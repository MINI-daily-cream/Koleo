import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { Link } from "react-router-dom";
import React, { useState } from 'react';
import { faUser, faMapMarkerAlt } from '@fortawesome/free-solid-svg-icons';

import {stationsData } from "./connections.js";

const TicketConfirmation = ({connectionsData, id }) => { // here there is USERS id
    const [name, setName] = useState('');
    const [surname, setSurname] = useState('');

    const handleNameChange = (e) => {
        setName(e.target.value);
    };
    const handleSurnameChange = (e) => {
        setSurname(e.target.value);
    };

    const handleBuyButtonClick = async () => {
        const requestBody = {
            connections: connectionsData,
            targetName: name,
            targetSurname: surname
        };
        try {
            const response = await fetch(`/buy/${id}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(requestBody)
            });

            if (response.ok) {
                console.log('Ticket purchased successfully.');
            } else {
                console.error('Failed to purchase ticket.');
            }
        } catch (error) {
            console.error('Network error:', error);
        }
    }

    const TravelTime = ({ date, timeDep, timeArr }) => {
        return (
            <div className="TravelTimeInfo">
                <div className="TicketInfoColumn">
                    <div className="TicketInfoColumnText">Odjazd</div>
                    <div className="TicketInfoColumnData">{timeDep}</div>
                </div>
                <div className="TicketInfoColumn">
                    <div className="TicketInfoColumnText">Przyjazd</div>
                    <div className="TicketInfoColumnData">{timeArr}</div>
                </div>
                <div className="TicketInfoColumn">
                    <div className="TicketInfoColumnText">Czas podróży</div>
                    <div className="TicketInfoColumnData">{timeDep}</div>
                </div>
                <div className="TicketInfoColumn">
                    <div className="TicketInfoColumnText">Data podróży</div>
                    <div className="TicketInfoColumnData">{date}</div>
                </div>
            </div>
        );
    };
    const TrainInfo = ({ trainNumber, wagonNumber, seatNumber }) => {
        return (
            <div className="TravelTimeInfo">
                <div className="TicketInfoColumn">
                    <div className="TicketInfoColumnText">Numer pociągu</div>
                    <div className="TicketInfoColumnData">{trainNumber}</div>
                </div>
                <div className="TicketInfoColumn">
                    <div className="TicketInfoColumnText">Numer wagonu</div>
                    <div className="TicketInfoColumnData">{wagonNumber}</div>
                </div>
                <div className="TicketInfoColumn">
                    <div className="TicketInfoColumnText">Numer miejsca</div>
                    <div className="TicketInfoColumnData">{seatNumber}</div>
                </div>
            </div>
        );
    };
    
    const ticketData = {
        date: '2024-04-25',
        timeDep: '10:00',
        timeArr: '12:30',
        //name: 'Jon',
        //surname: 'Some',
        trainNumber: '1234',
        finalStation: 'Destination',
        departureStation: 'Warszawa',
        arrivalStation: 'Gdańsk',
        wagonNumber: 'A12',
        seatNumber: '7',
    };
    // error here
    const firstConnection = connectionsData[0];
    const lastConnection = connectionsData[connectionsData.length - 1];

    // Accessing station names using station IDs
    const firstStartStationName = stationsData.find(station => station.id === firstConnection.startStationId)?.name;
    const firstEndStationName = stationsData.find(station => station.id === firstConnection.endStationId)?.name;
    const lastStartStationName = stationsData.find(station => station.id === lastConnection.startStationId)?.name;
    const lastEndStationName = stationsData.find(station => station.id === lastConnection.endStationId)?.name;
    return (
        <div>
            <div className="TicketInfoHeader">
                <p>Podsumowanie zakupu</p>
            </div>
            <form onSubmit={handleBuyButtonClick }>
                <div className="TravelDestInfo">
                      <div className="ticket-details">
                        <div className='text' id='od-do'>Od:</div>
                        <div className="icon">
                          <FontAwesomeIcon icon={faMapMarkerAlt} />
                        </div>
                        <div className='text'>{firstStartStationName}</div>
                        <div className='od-do-spacer' />
                        <div className='text' id='od-do'>Do:</div>
                        <div className="icon">
                          <FontAwesomeIcon icon={faMapMarkerAlt} />
                        </div>
                        <div className='text'>{lastEndStationName}</div>
                        </div>
                </div>
                <TravelTime date={ticketData.date} timeDep={ticketData.timeDep} timeArr={ticketData.timeArr} />
                <TrainInfo trainNumber={ticketData.trainNumber} wagonNumber={ticketData.wagonNumber} seatNumber={ticketData.seatNumber} />
                <div className="TravelTravelerInfo">
                    <h3>Dane podróżniczego</h3>
                    <div className="ticket-details">
                        <div className="icon">
                            <FontAwesomeIcon icon={faUser} />
                        </div>
                        <div>
                            <label htmlFor="name">Imię:</label>
                            <input
                                type="text"
                                id="name"
                                value={name}
                                onChange={handleNameChange}
                                required
                            />
                        </div>
                        <input type="text" placeholder="Nazwisko" value={surname} onChange={handleSurnameChange}></input>
                    </div>
                </div>
                <divc className="ButtonAligment">
                {/*TODO: set "to" prop*/}
                    <Link to="/"><button type="submit" className="ConfirmationButton">Zmień dane</button></Link>
                    <Link to="/"><button type="submit"
                        className="ConfirmationButton"
                        onClick={handleBuyButtonClick }
                    >Kupuję</button></Link>
                    </divc>
            </form>
        </div>
    );
}
 
export default TicketConfirmation;