import "../styles.css";
import {BsFillDoorOpenFill} from "react-icons/bs";

export const TopBar = () => {
    return (
        <div className="border-b px-4 mb-4 mt-2 pb-4 border-[--color-text-baby-blue]">
            <div className="flex items-center justify-between p-0.5">
                <div>
                    <span className="text-sm font-bold block">
                        Rooms
                    </span>
                    <span className="text-xs block">
                        All rooms
                    </span>
                </div>
                <button className="flex text-sm items-center gap-2 bg-gray-800 hover:bg-gray-700 hover:text-[--color-text-baby-blue] transition-colors rounded px-3 py-1.5">
                    <BsFillDoorOpenFill/>
                    <span>Create new</span>
                </button>
            </div>
        </div>
    )
}
