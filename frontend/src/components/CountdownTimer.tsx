import { useEffect, useState } from "react"
import { TimerProps } from "../Types"

export const CountdownTimer = ({ expiration, className }: TimerProps) => {
  const [timeLeft, setTimeLeft] = useState(calculateTimeLeft())

  function calculateTimeLeft() {
    const currentTime = new Date().getTime()
    const expirationTime = new Date(expiration).getTime()
    const difference = expirationTime - currentTime

    if (difference <= 0) {
      return { hours: 0, minutes: 0 }
    }

    return {
      hours: Math.floor(difference / (1000 * 60 * 60)),
      minutes: Math.floor((difference % (1000 * 60 * 60)) / (1000 * 60)),
    };
  }

  useEffect(() => {
    const timer = setInterval(() => {
      setTimeLeft(calculateTimeLeft());
    }, 60 * 1000); // Updates every minute

    return () => clearInterval(timer); // Cleanup on unmount
  }, [expiration]);

  return (
    <div className={className}>
      {timeLeft.hours}h {timeLeft.minutes}m
    </div>
  )
};

export default CountdownTimer
