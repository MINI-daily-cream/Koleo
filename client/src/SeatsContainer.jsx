import axios from 'axios';
import React, { useEffect, useState } from 'react'
import apiBaseUrl from './config';
import { useNavigate } from 'react-router-dom';

export default function SeatsContainer({connectionId, saveSeat}) {

    const navigate = useNavigate();

    const initialSeats = Array(6).fill(Array(30).fill('blue'));
    const [seats, setSeats] = useState(initialSeats);
    const [selectedSeat, setSelectedSeat] = useState('');
    const [selectedSeatTranslated, setSelectedSeatTranslated] = useState('');

    const letters = ['A', 'B', 'C', 'D', 'E', 'F'];

    function handleSelect(rowIndex, columnIndex) {
        if(seats[rowIndex][columnIndex] == 'red'){
            alert('Miejsce zajęte');
            fetchSeats();
            return;
        }

        const newSeats = seats.map(innerArray => innerArray.slice());
        var seat, newSelectedSeatTranslated;

        if(seats[rowIndex][columnIndex] == 'orange')
        {
            newSeats[rowIndex][columnIndex] = 'blue';
            seat = '';
            newSelectedSeatTranslated = seat;
        }
        else
        {
            if(selectedSeat != '')
            {
                console.log(Math.floor(Number(selectedSeatTranslated) / 6), Number(selectedSeatTranslated)%6);
                newSeats[Number(selectedSeatTranslated)%6][Math.floor(Number(selectedSeatTranslated) / 6)] = 'blue';
            }
            newSeats[rowIndex][columnIndex] = 'orange';
    
            seat = columnIndex + letters[rowIndex];
            newSelectedSeatTranslated = columnIndex*6 + rowIndex;
        }
        
        setSelectedSeatTranslated(newSelectedSeatTranslated);
        setSeats(newSeats);
        setSelectedSeat(seat);
        saveSeat(seat, newSelectedSeatTranslated)
    }

    const fetchSeats = async () => {
        try {
            const response = await axios.get(`${apiBaseUrl}/api/connectionseats/get/${connectionId}`);
            const newSeats = seats.map(innerArray => innerArray.slice())
            console.log(response.data);
            let index = 0;
            for(let j = 0; j < 30; j++){
                for(let i = 0; i < 6; i++){
                    if(response.data[index] === 0)
                        newSeats[i][j] = 'blue'
                    else 
                    {
                        newSeats[i][j] = 'red'
                        console.log(i, j);
                    }
                    index++;
                }
            }
            setSeats(newSeats);
        }
        catch(error) {
            console.log("some error");
        }
    };


    useEffect(() => {
        console.log(connectionId);
        

        fetchSeats();
    }, [])
    
    return (

        <div className="SeatsContainer">
            <h3>Wybierz miejsce</h3>
            <p>{connectionId}</p>
            <div className="Seats">
                <table className="SeatMap">
                    <tbody>
                        {
                            seats.map((row, rowIndex) => (
                                <React.Fragment key={rowIndex}>
                                    <tr>
                                        {
                                            row.map((value, columnIndex) => (
                                                <td key={columnIndex} onClick={() => handleSelect(rowIndex, columnIndex)}
                                                    style={{
                                                        // backgroundColor: seats[rowIndex][columnIndex]
                                                        backgroundColor: value
                                                    }}
                                                >
                                                
                                                </td>
                                            ))
                                        }
                                    </tr>
                                    {rowIndex === 2 && (
                                        <tr className="corridor"></tr>
                                    )}
                                </React.Fragment>
                            ))
                        }
                    </tbody>
                </table>
                <div className="SelectedSeatInfo">
                    Wybrano miejsce:
                    <p>{selectedSeat}</p>
                    <p>{selectedSeatTranslated}</p>
                </div>
            </div>
        </div>
    )
}
