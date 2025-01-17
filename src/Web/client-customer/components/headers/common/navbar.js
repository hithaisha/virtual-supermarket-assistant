import React, { useState, useEffect } from 'react'
import Link from 'next/link'
import { MENUITEMS } from '../../constant/menu'
import { Container, Row } from 'reactstrap'
import { useTranslation } from 'react-i18next'
import { useRouter } from 'next/router'

const NavBar = () => {
  const { t } = useTranslation()
  const [navClose, setNavClose] = useState({ right: '0px' })
  const router = useRouter()

  useEffect(() => {
    if (window.innerWidth < 750) {
      setNavClose({ right: '-410px' })
    }
    if (window.innerWidth < 1199) {
      setNavClose({ right: '-300px' })
    }
  }, [])

  const openNav = () => {
    setNavClose({ right: '0px' })
    if (router.asPath == '/layouts/Gym')
      document.querySelector('#topHeader').classList.add('zindex-class')
  }

  const closeNav = () => {
    setNavClose({ right: '-410px' })
    if (router.asPath == '/layouts/Gym')
      document.querySelector('#topHeader').classList.remove('zindex-class')
  }
  // eslint-disable-next-line

  const handleMegaSubmenu = (event) => {
    if (event.target.classList.contains('sub-arrow')) return

    if (
      event.target.parentNode.nextElementSibling.classList.contains(
        'opensubmegamenu',
      )
    )
      event.target.parentNode.nextElementSibling.classList.remove(
        'opensubmegamenu',
      )
    else {
      document.querySelectorAll('.menu-content').forEach(function (value) {
        value.classList.remove('opensubmegamenu')
      })
      event.target.parentNode.nextElementSibling.classList.add(
        'opensubmegamenu',
      )
    }
  }

  const [mainmenu, setMainMenu] = useState(MENUITEMS)

  useEffect(() => {
    const currentUrl = location.pathname
    MENUITEMS.filter((items) => {
      if (items.path === currentUrl) setNavActive(items)
      if (!items.children) return false
      items.children.filter((subItems) => {
        if (subItems.path === currentUrl) setNavActive(subItems)
        if (!subItems.children) return false
        subItems.children.filter((subSubItems) => {
          if (subSubItems.path === currentUrl) setNavActive(subSubItems)
        })
      })
    })
  }, [])

  const setNavActive = (item) => {
    MENUITEMS.filter((menuItem) => {
      if (menuItem != item) menuItem.active = false
      if (menuItem.children && menuItem.children.includes(item))
        menuItem.active = true
      if (menuItem.children) {
        menuItem.children.filter((submenuItems) => {
          if (submenuItems.children && submenuItems.children.includes(item)) {
            menuItem.active = true
            submenuItems.active = false
          }
        })
      }
    })

    setMainMenu({ mainmenu: MENUITEMS })
  }

  // Click Toggle menu
  const toggletNavActive = (item) => {
    if (!item.active) {
      MENUITEMS.forEach((a) => {
        if (MENUITEMS.includes(item)) a.active = false
        if (!a.children) return false
        a.children.forEach((b) => {
          if (a.children.includes(item)) {
            b.active = false
          }
          if (!b.children) return false
          b.children.forEach((c) => {
            if (b.children.includes(item)) {
              c.active = false
            }
          })
        })
      })
    }
    item.active = !item.active
    setMainMenu({ mainmenu: MENUITEMS })
  }

  const openMblNav = (event) => {
    if (event.target.classList.contains('sub-arrow')) return

    if (event.target.nextElementSibling.classList.contains('opensubmenu'))
      event.target.nextElementSibling.classList.remove('opensubmenu')
    else {
      document.querySelectorAll('.nav-submenu').forEach(function (value) {
        value.classList.remove('opensubmenu')
      })
      document
        .querySelector('.mega-menu-container')
        .classList.remove('opensubmenu')
      event.target.nextElementSibling.classList.add('opensubmenu')
    }
  }

  return (
    <div>
      <div className="main-navbar">
        <div id="mainnav">
          <div className="toggle-nav" onClick={openNav.bind(this)}>
            <i className="fa fa-bars sidebar-bar"></i>
          </div>
          <ul className="nav-menu" style={navClose}>
            <li className="back-btn" onClick={closeNav.bind(this)}>
              <div className="mobile-back text-end">
                <i className="fa fa-angle-right ps-2" aria-hidden="true"></i>
              </div>
            </li>
          </ul>
        </div>
      </div>
    </div>
  )
}

export default NavBar
