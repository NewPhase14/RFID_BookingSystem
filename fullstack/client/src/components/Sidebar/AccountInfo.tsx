import React from 'react'
import "../styles.css"
import {BiCog} from "react-icons/bi";

export const AccountInfo = () => {
    return (
        <div className="border-b mb-4 mt-2 pb-4 border-[--color-text-grey] ">
            <div className="flex p-0.5 relative gap-2 w-full items-center">
                <img
                src="https://api.dicebear.com/9.x/notionists/svg"
                alt="avatar"
                className="rounded size-8 shrink-0 bg-text-baby-blue shadow"
                />
                <div className="text-start">
                    <span className="text-sm font-bold block">
                        {localStorage.getItem("firstname") + " " + localStorage.getItem("lastname")}
                    </span>
                    <span className="text-xs block">
                        {localStorage.getItem("email")}
                    </span>
                </div>
                <button className="px-2 py-1.5 bg-[--color-background-grey] hover:bg-[--color-button-grey] transition-colors rounded">
                    <BiCog/>
                </button>
            </div>
        </div>
    )
}
