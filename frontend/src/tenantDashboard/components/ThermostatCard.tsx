import { useState } from 'react';
import Card from '../../components/Card';

const ThermostatCard = () => {
    const [temperature, setTemperature] = useState(72);

    const increaseTemp = () => {
        setTemperature(prev => Math.min(prev + 1, 85));
    };

    const decreaseTemp = () => {
        setTemperature(prev => Math.max(prev - 1, 65));
    };

    return (
        <div>
            <h3 className="text-lg font-medium text-black mb-2">Thermostat</h3>
            <Card className="bg-white rounded-xl p-6 relative">
                <div className="absolute top-6 right-6">
                    <span className="bg-[#89CFF0] text-black px-3 py-1 rounded-lg text-md font-bold">
                        F
                    </span>
                </div>

                <div className="flex items-center">
                    <span className="text-4xl font-bold mr-4">{temperature}°</span>
                    <div className="flex flex-col gap-2">
                        <button
                            onClick={increaseTemp}
                            className="hover:bg-gray-100 p-2 rounded transition-colors"
                            aria-label="Increase temperature"
                        >
                            <svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                                <path d="M12 20L4 12L12 4" stroke="#3C4144" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round" transform="rotate(90 12 12)" />
                            </svg>
                        </button>
                        <button
                            onClick={decreaseTemp}
                            className="hover:bg-gray-100 p-2 rounded transition-colors"
                            aria-label="Decrease temperature"
                        >
                            <svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                                <path d="M12 4L20 12L12 20" stroke="#3C4144" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round" transform="rotate(90 12 12)" />
                            </svg>
                        </button>
                    </div>
                </div>
            </Card>
        </div>
    );
};

export default ThermostatCard;