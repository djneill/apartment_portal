export default function LeaseCountdown() {
  return (
    <div className="w-full bg-black font-heading rounded-2xl p-5">
      <div className="flex items-center justify-between mb-4">
        <h1 className="text-white">Lease Countdown</h1>
        <button className="bg-white text-black rounded-full px-4 py-2 font-semibold text-sm">Send Notification</button>
      </div>
      <div className="flex text-white items-center space-x-5">
        <div className="flex flex-col items-center">
          <p className="text-xl">18</p>
          <p className="text-[#919397] text-sm">Days</p>
        </div>
        <div className="border-l-1 h-5 border-white "></div>

        <div className="flex flex-col items-center">
          <p className="text-xl">23</p>
          <p className="text-[#919397] text-sm">Hours</p>
        </div>
        <div className="border-l-1 h-5 border-white "></div><div className="flex flex-col items-center">
          <p className="text-xl">58</p>
          <p className="text-[#919397] text-sm">Minutes</p>
        </div>
        <div className="border-l-1 h-5 border-white "></div><div className="flex flex-col items-center">
          <p className="text-xl">16</p>
          <p className="text-[#919397] text-sm">Seconds</p>
        </div>

      </div>
    </div>)
}
