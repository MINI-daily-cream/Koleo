import React, { useEffect, useState } from 'react'

function SeatSelection({onClick}) {

    var initialSeats = Array(6).fill(Array(30).fill('blue'));
    var initialSeats = Array(6).fill(null).map(() => Array(30).fill('blue'))
    const [seats, setSeats] = useState(initialSeats);

    function selectSeat(rowIndex, columnIndex){
        initialSeats = seats.slice();
        initialSeats[rowIndex][columnIndex] = 'orange';        
        setSeats(initialSeats);
        onClick(columnIndex);
    }

    return (
        <div className='SeatSelection'>
            {/* Hello */}
            <p>dd</p>
            <table>
                <tbody>
                    {seats.map((row, rowIndex) => (
                    <>
                        <tr>
                            {row.map((color, columnIndex) => (
                                <td style={{backgroundColor: color}} onClick={() => selectSeat(rowIndex, columnIndex)}></td>
                            ))}
                        </tr>
                        {rowIndex == 2 ? <br/>: null}
                    </>
                    ))}
                </tbody>
            </table>
        </div>
    )
}

export default SeatSelection

