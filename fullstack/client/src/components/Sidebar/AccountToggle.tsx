import React from 'react'
import "../styles.css"
import {LuFileCog} from "react-icons/lu";
import {BiCog} from "react-icons/bi";

export const AccountToggle = () => {
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
                        Jeppe Baden
                    </span>
                    <span className="text-xs block">
                        jeppedev@test.dk
                    </span>
                </div>
                <button className="ml-8 px-2 py-1.5 bg-[--color-background-grey] hover:bg-[--color-button-grey] transition-colors rounded">
                    <BiCog/>
                </button>
            </div>
        </div>
    )
}
