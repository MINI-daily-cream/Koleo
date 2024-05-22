import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { Link, useLocation, useNavigate, useParams } from "react-router-dom";
import React, { useState, useEffect } from 'react';
import { faUser, faMapMarkerAlt } from '@fortawesome/free-solid-svg-icons';
import TimeComponent from "./sharedComponents/TimeComponent.jsx";
import apiBaseUrl from "./config.js";
import axios from 'axios'
import SeatSelection from "./SeatSelection.jsx";
const TicketConfirmation = ({ navigation, route }) => { // here there is USERS id
    const navigate = useNavigate();
    const { state } = useLocation();
    const [name, setName] = useState('');
    const [surname, setSurname] = useState('');

    const location = useLocation();

    const [mainConnection, setmainConnection] = useState(
        { startStation: '', endStation: '', startDate: '', endDate: '', startTime: '', endTime: '' }
    );
    const [userId, setUserId] = useState(localStorage.getItem('id'));
    const [jwtToken, setJwtToken] = useState(localStorage.getItem('jwtToken'));

    function getUserData() {
        const fetchData = async () => {
            try {
                const response = await axios.get(`${apiBaseUrl}/api/Account/${userId}`, {
                    headers: {
                        'Content-Type': 'application/json',
                        'Authorization': `Bearer ${jwtToken}`
                    }
                });
                // setUserInfo(response.data);
                setName(response.data.name);
                setSurname(response.data.surname);
            }
            catch (error) {
                if (error === 'Bad request') {
                    console.error('user exists');
                } else {
                    console.error('An error occurred:', error);
                }
            }
        };
        fetchData();
    }


    useEffect(() => {
        console.log('pa na to');
        console.log(state);
        setmainConnection(state);
        getUserData();
    }, []);

    const handleNameChange = (e) => {
        setName(e.target.value);
    };

    const handleSurnameChange = (e) => {
        setSurname(e.target.value);
    };

    const handleBuyButtonClick = async () => {
        console.log(userId);
        console.log(mainConnection.id);

        const requestBody = {
            connectionIds: [mainConnection.id],
            targetName: name,
            targetSurname: surname
        };

        try {
            const response = await axios.post(`${apiBaseUrl}/api/Ticket/buy/${userId}`, requestBody, {
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${jwtToken}`
                }
            });
            if (response.status === 200) {
                console.log('Ticket purchased successfully.');
            } else {
                console.error('Failed to purchase ticket.');
            }
        } catch (error) {
            console.error('Network error:', error);
        }
        navigate("/account/tickets");
    };

    const ByStations = () => {
        return (
            <div className="TravelTimeInfo">
                <div className="separator">
                    <h3>Przez stacje:</h3>
                    <hr></hr>
                    <div className="stationItem">
                        <FromStationToStation startStation={mainConnection.startStation} endStation={mainConnection.endStation}></FromStationToStation>
                        <TrainInfo trainNumber={mainConnection.providerName} wagonNumber={ticketData.wagonNumber} seatNumber={ticketData.seatNumber} />
                        <div className="TravelTimeInfo">
                            <div className="TicketInfoColumn">
                                <div className="TicketInfoColumnText">Odjazd</div>
                                <div className="TicketInfoColumnData">
                                    <TimeComponent time={mainConnection.startTime}></TimeComponent>
                                </div>
                            </div>
                            <div className="TicketInfoColumn">
                                <div className="TicketInfoColumnText">Przyjazd</div>
                                <div className="TicketInfoColumnData">
                                    <TimeComponent time={mainConnection.endTime}></TimeComponent>
                                </div>
                            </div>
                            <div className="TicketInfoColumn">
                                <div className="TicketInfoColumnText">Czas podróży</div>
                                <div className="TicketInfoColumnData">{mainConnection.duration}</div>
                            </div>
                            <div className="TicketInfoColumn">
                                <div className="TicketInfoColumnText">Data odjazdu</div>
                                <div className="TicketInfoColumnData">{mainConnection.startDate}</div>
                            </div>
                            <div className="TicketInfoColumn">
                                <div className="TicketInfoColumnText">Data przyjazdu</div>
                                <div className="TicketInfoColumnData">{mainConnection.endDate}</div>
                            </div>
                        </div>
                    </div>
                    <hr></hr>
                </div>
            </div>
        );
    };
    const TravelTime = ({ startDate, endDate, timeDep, timeArr }) => {
        return (
            <div className="TravelTimeInfo">
                <div className="TicketInfoColumn">
                    <div className="TicketInfoColumnText">Odjazd</div>
                    <div className="TicketInfoColumnData">
                        <TimeComponent time={timeDep}></TimeComponent>
                    </div>
                </div>
                <div className="TicketInfoColumn">
                    <div className="TicketInfoColumnText">Przyjazd</div>
                    <div className="TicketInfoColumnData">
                        <TimeComponent time={timeArr}></TimeComponent>
                    </div>
                </div>
                <div className="TicketInfoColumn">
                    <div className="TicketInfoColumnText">Data odjazdu</div>
                    <div className="TicketInfoColumnData">{startDate}</div>
                </div>
                <div className="TicketInfoColumn">
                    <div className="TicketInfoColumnText">Data przyjazdu</div>
                    <div className="TicketInfoColumnData">{endDate}</div>
                </div>
            </div>
        );
    };
    const TrainInfo = ({ trainNumber, wagonNumber, seatNumber }) => {
        return (
            <div className="TravelTimeInfo">
                <div className="TicketInfoColumn">
                    <div className="TicketInfoColumnText">Przewoźnik</div>
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
    const FromStationToStation = ({ startStation, endStation }) => {
        return (
            <div className="TravelDestInfo">
                <div className="ticket-details">
                    <div className='text' id='od-do'>Od:</div>
                    <div className="icon">
                        <FontAwesomeIcon icon={faMapMarkerAlt} />
                    </div>
                    <div className='text'>{startStation}</div>
                    <div className='od-do-spacer' />
                    <div className='text' id='od-do'>Do:</div>
                    <div className="icon">
                        <FontAwesomeIcon icon={faMapMarkerAlt} />
                    </div>
                    <div className='text'>{endStation}</div>
                </div>
            </div>
        );
    };
    //xddd
    const ticketData = {
        wagonNumber: 'A12',
        seatNumber: '7',
    };
    return (
        <div>
            <div className="TicketInfoHeader">
                <p>Podsumowanie zakupu</p>
            </div>
            <form onSubmit={handleBuyButtonClick}>

                <FromStationToStation
                    startStation={mainConnection.sourceCity}
                    endStation={mainConnection.destinationCity}>
                </FromStationToStation>
                <TravelTime startDate={mainConnection.startDate}
                    endDate={mainConnection.endDate}
                    timeDep={mainConnection.startTime}
                    timeArr={mainConnection.endTime} />

                <ByStations />

                <SeatSelection/>


                <div className="TravelTravelerInfo">
                    <h3>Dane podróżniczego</h3>
                    <div className="ticket-details">
                        <div className="icon">
                            <FontAwesomeIcon icon={faUser} />
                        </div>
                        <div className="TravelerInfoInput">
                            <label htmlFor="name">Imię:</label>
                            <input
                                type="text"
                                id="name"
                                value={name}
                                onChange={handleNameChange}
                                required
                            />
                        </div>
                        <div className="TravelerInfoInput">
                            <label htmlFor="surname">Nazwisko:</label>
                            <input
                                type="text"
                                id="surname"
                                value={surname}
                                onChange={handleSurnameChange}
                                required
                            />
                        </div>
                        {/* <input type="text" placeholder="Nazwisko" value={surname} onChange={handleSurnameChange}></input> */}
                    </div>
                </div>
                <div className="ButtonAligment">
                    {/*TODO: set "to" prop*/}
                    <Link to="/FoundConnections"><button type="button" className="ConfirmationButton">Wróć</button></Link>
                    <button type="button"
                        className="ConfirmationButton"
                        onClick={handleBuyButtonClick}
                    >Kupuję</button>
                </div>
            </form>
        </div>
    );
}

export default TicketConfirmation;